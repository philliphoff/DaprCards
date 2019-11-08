import React, { useState, useCallback, useContext } from 'react';
import PlayContext from '../components/play/playContext';
import Stepper from '@material-ui/core/Stepper';
import DeckSelector from '../components/play/deckSelector';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import DaprAppBar from '../components/daprAppBar';
import Button from '@material-ui/core/Button';
import GameConfirmation from '../components/play/gameConfirmation';
import { postAsync } from '../util/fetchAsync';
import UserContext from '../components/userContext';

const steps =  [
    {
        label: 'Select deck',
        content: () => <DeckSelector />
    },
    {
        label: 'Confirm game',
        content: () => <GameConfirmation />
    }
];

const startGameHelper =
    async (userId: string, deckId: string, startGame: (string) => void) => {
        const gameId = await postAsync<string>('/api/startGame', deckId, { 'x-user-id': userId });
        
        startGame(gameId);
    };

export const Play =
    (props) => {
        const { userId, startGame } = useContext(UserContext);
        const [ deckId, setDeckId ] = useState();
        const [ step, setStep ] = useState(0);

        const onBack = useCallback(
            () => {
                setStep(step - 1);
            },
            [step, setStep]);

        const onNext = useCallback(
            () => {
                if (step + 1 < steps.length) {
                    setStep(step + 1);
                } else {
                    startGameHelper(userId, deckId, startGame);
                }
            },
            [step, setStep, startGame]);
        
        const isBackDisabled = step === 0;
        const isNextDisabled = deckId === undefined;

        return (
            <PlayContext.Provider value={{ deckId, setDeckId }}>
                <div>
                    <DaprAppBar />
                    <Stepper>
                        {
                            steps.map(step => (
                                <Step key={step.label}>
                                    <StepLabel>{step.label}</StepLabel>
                                </Step>
                            ))
                        }
                    </Stepper>
                    {
                        steps[step].content()
                    }
                    <div>
                        <Button disabled={isBackDisabled} onClick={onBack} variant="contained">Back</Button>
                        <Button color="primary" disabled={isNextDisabled} onClick={onNext} variant="contained">{ step === steps.length - 1 ? 'Start' : 'Next' }</Button>
                    </div>
                </div>
            </PlayContext.Provider>
        );
    };

export default Play;
