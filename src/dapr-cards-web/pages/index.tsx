import React from 'react'
import Head from 'next/head'
import DaprAppBar from '../components/daprAppBar'
import { makeStyles } from '@material-ui/styles';
import UserContext from '../components/userContext';
import { Link } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1
    }
}));

const Home = (props) => {
  const classes = useStyles(props);
  const { isLoggedIn } = React.useContext(UserContext);

  return (
    <div className={classes.root}>
        <Head>
              <title>Home</title>
              <link rel='icon' href='/favicon.ico' />
        </Head>
        <DaprAppBar />
        {
            isLoggedIn
                ? <Link href="/play">Play Game</Link>
                : null
        }
    </div>
  );
}

export default Home
