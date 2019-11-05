import { createContext } from 'react';

type UserContextProps = {
    userId?: string;
    isLoggedIn: boolean;
    logInStart: () => void;
    logInEnd: (userId: string) => void;
    logOut: () => void;
};

const UserContext = createContext<UserContextProps>({
    isLoggedIn: false,
    logInStart: undefined,
    logInEnd: undefined,
    logOut: undefined
});

export default UserContext;
