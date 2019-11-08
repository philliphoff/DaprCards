import React, { useContext, useState, useCallback } from 'react';
import PlayContext from './playContext';
import UserContext from '../userContext';
import useEffectAsync from '../../useEffectAsync';
import { getAsync } from '../../util/fetchAsync';

export const Deck =
    (props: { deckId: string, name: string }) => {
        const { deckId, name } = props;
        const { setDeckId } = useContext(PlayContext);
        const onClick = useCallback(
            () => {
                setDeckId(deckId);
            },
            [deckId, setDeckId]);

        return (
            <div onClick={onClick}>
                { name }
            </div>
        );
    };

type UserDeck = {
    deckId: string;
    name: string;
};

export const DeckSelector = () => {
    const { userId } = useContext(UserContext);
    //const { deckId, setDeckId } = useContext(PlayContext);
    const [ decks, setDecks ] = useState<UserDeck[]>([]);

    useEffectAsync(
        async () => {
            if (userId) {
                const deckIds = await getAsync<UserDeck[]>(
                    '/api/decks',
                    {
                        'X-User-ID': userId
                    });
                    
                setDecks(deckIds);
            }
        },
        [userId, setDecks]);

    return (
        <>
            { decks.map(deck => (<Deck key={deck.deckId} deckId={deck.deckId} name={deck.name} />)) }
        </>
    );
};

export default DeckSelector;
