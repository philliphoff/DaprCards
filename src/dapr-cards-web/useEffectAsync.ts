import { useEffect } from 'react';

export const useEffectAsync =
    (effect: () => Promise<void>, deps?: any[]) => {
        return useEffect(
            () => {
                effect();
            },
            deps);
    };

export default useEffectAsync;