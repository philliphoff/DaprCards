import React, { useState } from 'react';
import Head from 'next/head';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/styles';
import Paper from '@material-ui/core/Paper';
import TextField from '@material-ui/core/TextField';
import Typography from '@material-ui/core/Typography';
import { DaprAppBar } from '../components/daprAppBar';
import fetch from 'isomorphic-unfetch';
import UserContext from '../components/userContext';

const useStyles = makeStyles(theme => ({
    grid: {
        justify: 'center'
    },
    paper: {
        width: 500
    },
    signInRoot: {
        display: 'flex',
        flexDirection: 'column'
    }
}));

const Login = (props) => {
    const classes = useStyles(props);
    const { logInEnd } = React.useContext(UserContext);

    const [signInEmail, setSignInEmail] = useState('');
    const [signUpEmail, setSignUpEmail] = useState('');

    const onSignIn =
        async (e: React.MouseEvent) => {
            var response = await fetch(
                '/api/signin',
                {
                    body: JSON.stringify(signInEmail),
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    method: 'POST'
                });

            var userId = await response.json();

            logInEnd(userId);
        };

    const onSignUp =
        async () => {
            var response = await fetch(
                '/api/signup',
                {
                    body: JSON.stringify(signUpEmail),
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    method: 'POST'
                });

            var userId = await response.json();

            logInEnd(userId);
        };

    return (
        <div>
            <Head>
                <title>Login</title>
            </Head>
            <DaprAppBar enableLogIn={false} />
            <Grid container className={classes.grid} justify="center" spacing={2}>
                <Grid item>
                    <Paper className={classes.paper}>
                        <div>
                            <Typography>First time here? Sign up!</Typography>
                            <TextField label="Email" onChange={e => setSignUpEmail(e.target.value)} />
                            <Button variant="contained" color="primary" onClick={onSignUp}>
                                Sign up
                            </Button>
                        </div>
                    </Paper>
                </Grid>
                <Grid item>
                    <Paper className={classes.paper}>
                        <div className={classes.signInRoot}>
                            <Typography>Already a gamer? Sign in!</Typography>
                            <TextField label="Email" onChange={e => setSignInEmail(e.target.value)} />
                            <Button variant="contained" color="primary" onClick={onSignIn}>
                                Sign in
                            </Button>
                        </div>
                    </Paper>
                </Grid>
            </Grid>
        </div>
    );
};

export default Login;
