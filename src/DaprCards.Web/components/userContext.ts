import { createContext } from 'react';

type UserContextProps = {
    gameId?: string;
    userId?: string;
    isLoggedIn: boolean;
    isPlaying: boolean;
    logInStart: () => void;
    logInEnd: (userId: string) => void;
    logOut: () => void;
    startGame: (gameId: string) => void;
};

const UserContext = createContext<UserContextProps>({
    isLoggedIn: false,
    isPlaying: false,
    logInStart: undefined,
    logInEnd: undefined,
    logOut: undefined,
    startGame: undefined
});

export default UserContext;
