import { createContext } from 'react';

type UserContextProps = {
    userId?: string;
    logInStart: () => void;
    logInEnd: (userId: string) => void;
    logOut: () => void;
};

const UserContext = createContext<UserContextProps>({
    logInStart: undefined,
    logInEnd: undefined,
    logOut: undefined
});

export default UserContext;
