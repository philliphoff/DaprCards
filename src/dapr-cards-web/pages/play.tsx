import React, { useState } from 'react';
import PlayContext from '../components/play/playContext';
import Stepper from '@material-ui/core/Stepper';
import DeckSelector from '../components/play/deckSelector';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import DaprAppBar from '../components/daprAppBar';

const steps =  [
    {
        label: 'Select deck',
        content: () => <DeckSelector />
    },
    {
        label: 'Confirm game',
        content: () => null
    }
];

export const Play = (props) => {
    const [ deckId, setDeckId ] = useState();
    const [ step, setStep ] = useState<number>(0);

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
            </div>
        </PlayContext.Provider>
    );
};

export default Play;
