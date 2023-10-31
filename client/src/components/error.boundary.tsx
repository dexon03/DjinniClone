import React from 'react';
import { Dialog, DialogTitle, DialogContent, DialogActions, Button } from '@mui/material';

class ErrorBoundary extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            hasError: false,
        };
    }

    static getDerivedStateFromError(error) {
        // Update state so the next render will show the fallback UI.
        return { hasError: true };
    }

    componentDidCatch(error, errorInfo) {
        // You can also log the error to an error reporting service
        console.log(error, errorInfo);
    }

    handleReset = () => {
        this.setState({ hasError: false });
    };

    render() {
        if (this.state.hasError) {
            return (
                <Dialog open={this.state.hasError} onClose={this.handleReset}>
                    <DialogTitle>Error</DialogTitle>
                    <DialogContent>
                        Something went wrong. Please try again.
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleReset} color="primary">
                            Close
                        </Button>
                    </DialogActions>
                </Dialog>
            );
        }

        return this.props.children;
    }
}

export default ErrorBoundary;