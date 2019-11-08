import * as React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/styles';
import UserContext from './userContext';

const useStyles = makeStyles(theme => ({
    title: {
        flexGrow: 1
    }
}));

type DaprAppBarProps = {
    enableLogIn?: boolean;
};

export const DaprAppBar = (props: DaprAppBarProps) => {
    const classes = useStyles(props);
    const { userId, logInStart, logOut } = React.useContext(UserContext);

    const onClick =
        e => {
            if (userId) {
                logOut();
            } else {
                logInStart();
            }
        };

    return (
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" className={classes.title}>
                    Dapr Cards
                </Typography>
                {
                    props.enableLogIn === undefined || props.enableLogIn
                        ?
                        <Button color="inherit" onClick={onClick}>
                            { userId ? 'Logout' : 'Login' }
                        </Button>
                        : null
                }
            </Toolbar>
        </AppBar>
    );
};

export default DaprAppBar;
