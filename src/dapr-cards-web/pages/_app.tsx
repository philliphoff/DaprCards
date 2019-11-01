import React from 'react';
import App from 'next/app';
import Head from 'next/head';
import { ThemeProvider } from '@material-ui/styles';
import CssBaseline from '@material-ui/core/CssBaseline';
import theme from '../theme';
import UserContext from '../components/userContext';
import Router from 'next/router';

export default class MyApp extends App<{}, {}, { userId?: string }> {
  constructor(props) {
    super(props);

    this.state = {};
  }

  componentDidMount() {
    // Remove the server-side injected CSS.
    const jssStyles = document.querySelector('#jss-server-side');
    if (jssStyles) {
      jssStyles.parentNode.removeChild(jssStyles);
    }

    const userId = localStorage.getItem('user-id');
    if (userId) {
      this.setState({
        userId
      });
    }
  }

  render() {
    const { Component, pageProps } = this.props;
    const { userId } = this.state;

    return (
      <React.Fragment>
        <Head>
          <title>My page</title>
        </Head>
        <ThemeProvider theme={theme}>
          {/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
          <CssBaseline />
          <UserContext.Provider value={{ userId, logInStart: this.logInStart, logInEnd: this.logInEnd, logOut: this.logOut }}>
            <Component {...pageProps} />
          </UserContext.Provider>
        </ThemeProvider>
      </React.Fragment>
    );
  }

  private readonly logInStart = () => {
    Router.push('/login');
  }

  private readonly logInEnd = (userId: string) => {
    this.setState({
      userId
    });

    Router.back();
  }

  private readonly logOut = () => {
    this.setState({
      userId: undefined
    });
  }
}