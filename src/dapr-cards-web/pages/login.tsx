import React from 'react';
import Head from 'next/head';
import Button from '@material-ui/core/Button';

const Login = () => (
    <div>
        <Head>
            <title>Login</title>
        </Head>
        <Button variant="contained" color="primary">
            Login
        </Button>
    </div>
);

export default Login;
