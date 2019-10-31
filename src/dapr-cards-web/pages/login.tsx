import React from 'react';
import Head from 'next/head';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/styles';
import Paper from '@material-ui/core/Paper';
import TextField from '@material-ui/core/TextField';
import Typography from '@material-ui/core/Typography';
import { DaprAppBar } from '../components/daprAppBar';

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
                            <Button variant="contained" color="primary">
                                Sign up
                            </Button>
                        </div>
                    </Paper>
                </Grid>
                <Grid item>
                    <Paper className={classes.paper}>
                        <div className={classes.signInRoot}>
                            <Typography>Already a gamer? Sign in!</Typography>
                            <TextField label="Email" />
                            <TextField label="Password" />
                            <Button variant="contained" color="primary">
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
