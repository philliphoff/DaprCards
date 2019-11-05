import React, { useContext, useState } from 'react';
import PlayContext from './playContext';
import UserContext from '../userContext';
import useEffectAsync from '../../useEffectAsync';
import { getAsync } from '../../util/fetchAsync';

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
                    
                setDecks(decks);
            }
        },
        [userId]);

    return (
        <>
            { decks.map(deckId => (<div key={deckId}>{deckId}</div>)) }
        </>
    );
};

export default DeckSelector;
