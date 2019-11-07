import React, { useContext, useState, useCallback } from 'react';
import PlayContext from './playContext';
import UserContext from '../userContext';
import useEffectAsync from '../../useEffectAsync';
import { getAsync } from '../../util/fetchAsync';

export const Deck =
    (props: { deckId: string }) => {
        const { deckId } = props;
        const { setDeckId } = useContext(PlayContext);
        const onClick = useCallback(
            () => {
                setDeckId(deckId);
            },
            [deckId, setDeckId]);

        return (
            <div onClick={onClick}>
                { deckId }
            </div>
        );
    };

export const DeckSelector = () => {
    const { userId } = useContext(UserContext);
    //const { deckId, setDeckId } = useContext(PlayContext);
    const [ decks, setDecks ] = useState<string[]>([]);

    useEffectAsync(
        async () => {
            if (userId) {
                const deckIds = await getAsync<string[]>(
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
            { decks.map(deckId => (<Deck key={deckId} deckId={deckId} />)) }
        </>
    );
};

export default DeckSelector;
