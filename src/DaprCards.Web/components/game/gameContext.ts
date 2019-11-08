import { createContext } from 'react';

export type GameCard = {
    cardId: string;
    isPlayed: boolean;
    value: number;
};

export type GamePlayer = {
    cards?: GameCard[];
    userId: string;
};

export type GameDetails = {
    players?: GamePlayer[];
};

type GameContextProps = {
    details?: GameDetails;
    selectedCardId?: string;
    setDetails: (details: GameDetails) => void;
    setSelectedCardId: (cardId: string) => void;
};

const GameContext = createContext<GameContextProps>({
    setDetails: undefined,
    setSelectedCardId: undefined
});

export default GameContext;
