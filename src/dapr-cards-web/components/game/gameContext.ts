import { createContext } from 'react';

type GameContextProps = {
    selectedCardId?: string;
    setSelectedCardId: (cardId: string) => void;
};

const GameContext = createContext<GameContextProps>({
    setSelectedCardId: undefined
});

export default GameContext;
