import React from 'react'
import Head from 'next/head'
import DaprAppBar from '../components/daprAppBar'
import { makeStyles } from '@material-ui/styles';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1
    }
}));

const Home = (props) => {
  const classes = useStyles(props);

  return (
      <div className={classes.root}>
          <Head>
              <title>Home</title>
              <link rel='icon' href='/favicon.ico' />
          </Head>
          <DaprAppBar />
      </div>
  );
}

export default Home
