using System;
using System.Collections.Generic;

using Interfaces.Delegates.Convert;
using Interfaces.Delegates.Confirm;

using Models.ArgsTokens;

namespace Delegates.Convert.ArgsTokens
{
    public class ConvertTokensToLikelyTypedTokensDelegate :
        IConvertDelegate<
            IEnumerable<string>,
            IEnumerable<(string, Tokens)>>
    {
        private IConfirmDelegate<(string, Tokens)> confirmLikelyTokenTypeDelegate;

        public ConvertTokensToLikelyTypedTokensDelegate(
            IConfirmDelegate<(string, Tokens)> confirmLikelyTokenTypeDelegate)
        {
            this.confirmLikelyTokenTypeDelegate = confirmLikelyTokenTypeDelegate;
        }

        public IEnumerable<(string, Tokens)> Convert(IEnumerable<string> untypedTokens)
        {
            var groups = new Queue<Groups>(TokensGroups.ParsingExpectations.Keys);
            var tokens = new Queue<string>(untypedTokens);

            var currentGroup = groups.Count > 0 ?
                groups.Dequeue() :
                Groups.Unknown;
            var currentToken = tokens.Count > 0 ?
                tokens.Dequeue() :
                null;

            // Determining token types makes few assumptions:
            // (1) All token type belong to one of the groups (TokensGroups.ParsingExpecations.Keys)
            // (2) Token groups are sequential - if the token doesn't match any likely type in the group
            // this and every following token can only be of the type in one of the following groups
            // (3) Detemined type can be either precise - when the exact value can be matched
            // or "likely", when we are only checking the condition (prefix).
            // (4) User-provided attribute values can't be validated other than placement order,
            // so when assuming we've got to an attribute title/values groups - 
            // anything we can't determine type for - would be considered attribute value

            while (currentToken != null)
            {
                (string Token, Tokens Type) typedToken =
                    new ValueTuple<string, Tokens>(currentToken, Tokens.Unknown);

                foreach (var type in TokensGroups.ParsingExpectations[currentGroup])
                    if (confirmLikelyTokenTypeDelegate.Confirm((currentToken, type)))
                    {
                        typedToken.Type = type;
                        yield return typedToken;
                        break;
                    }

                // If token type was NOT determined 
                if (typedToken.Type == Tokens.Unknown)
                {
                    // Progress to the next group, if there are more groups...
                    if (groups.Count > 0) currentGroup = groups.Dequeue();
                    else
                    {
                        // ...or if no more groups are available - we need to return
                        // all remaining tokens are Unknown, given assumption (2) above...
                        if (!string.IsNullOrEmpty(currentToken))
                            yield return (currentToken, Tokens.Unknown);
                        foreach (var token in tokens)
                            yield return (token, Tokens.Unknown);

                        // ...and stop progression
                        currentToken = null;
                    }
                }

                // If the type was determined - we either:
                // - progress to the next token
                // - stop progression is there are no more token
                if (typedToken.Type != Tokens.Unknown)
                    currentToken = tokens.Count > 0 ?
                        tokens.Dequeue() :
                        null;

            }
        }
    }
}