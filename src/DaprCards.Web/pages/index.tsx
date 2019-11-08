import React, { useCallback } from 'react'
import Head from 'next/head'
import DaprAppBar from '../components/daprAppBar'
import { makeStyles } from '@material-ui/styles';
import UserContext from '../components/userContext';
import Button from '@material-ui/core/Button';
import Router from 'next/router';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1
    },
    contentRoot: {
        display: 'flex',
        justifyContent: 'center'
    },
    playButton: {
        marginTop: 20
    }
}));

const Home = (props) => {
  const classes = useStyles(props);
  const { isLoggedIn } = React.useContext(UserContext);
  const onClick = useCallback(
      () => {
          Router.push('/play');
      },
      []);

  return (
    <div className={classes.root}>
        <Head>
              <title>Home</title>
              <link rel='icon' href='/favicon.ico' />
        </Head>
        <DaprAppBar />
        <div className={classes.contentRoot}>
          {
            isLoggedIn
            ? <Button className={classes.playButton} color="primary" onClick={onClick} variant="contained">Play Game</Button>
            : null
          }
        </div>
    </div>
  );
}

export default Home
