import { createContext } from 'react';

type PlayContextProps = {
    deckId?: string;
    setDeckId: (deckId: string) => void;
};

const UserContext = createContext<PlayContextProps>({
    setDeckId: undefined
});

export default UserContext;
