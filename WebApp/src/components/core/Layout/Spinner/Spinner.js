import './Spinner.scss';

const SpinnerElement = () => {
    return (
        <div class="lds-roller">
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
            <div></div>
        </div>
    )
}

const Spinner = props => {
    return (
        <React.Fragment>
            {ReactDOM.createPortal(
                <SpinnerElement />,
                document.querySelector("#loading-root")
            )}
        </React.Fragment>
    );
}

export default Spinner;