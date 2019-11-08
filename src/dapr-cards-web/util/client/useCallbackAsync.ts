import { useCallback } from "react";

export const useCallbackAsync =
    (callback: () => Promise<void>, deps: any[]) => {
        return useCallback(
            () => {
                callback();
            },
            deps);
    };

export default useCallbackAsync;
