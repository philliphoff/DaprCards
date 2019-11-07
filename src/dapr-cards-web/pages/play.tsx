import React, { useState, useCallback } from 'react';
import PlayContext from '../components/play/playContext';
import Stepper from '@material-ui/core/Stepper';
import DeckSelector from '../components/play/deckSelector';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import DaprAppBar from '../components/daprAppBar';
import Button from '@material-ui/core/Button';
import GameConfirmation from '../components/play/gameConfirmation';

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

export const Play =
    (props) => {
        const [ deckId, setDeckId ] = useState();
        const [ step, setStep ] = useState(0);

        const onBack = useCallback(
            () => {
                setStep(step - 1);
            },
            [step, setStep]);

        const onNext = useCallback(
            () => {
                setStep(step + 1);
            },
            [step, setStep]);
        
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
