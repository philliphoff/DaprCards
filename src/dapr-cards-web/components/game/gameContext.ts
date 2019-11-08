import { createContext } from 'react';

export type GameCard = {
    cardId: string;
    isPlayed: boolean;
    value: number;
};

type GameContextProps = {
    cards: GameCard[];
    selectedCardId?: string;
    setCards: (cards: GameCard[]) => void;
    setSelectedCardId: (cardId: string) => void;
};

const GameContext = createContext<GameContextProps>({
    cards: [],
    setCards: undefined,
    setSelectedCardId: undefined
});

export default GameContext;
