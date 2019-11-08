import { createContext } from 'react';

type GameContextProps = {
    gameId?: string;
    setGameId: (deckId: string) => void;
};

const GameContext = createContext<GameContextProps>({
    setGameId: undefined
});

export default GameContext;
