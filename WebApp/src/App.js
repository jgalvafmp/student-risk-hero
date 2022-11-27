import Routes from './components/core/Routes/Routes';
import { AuthContextProvider } from './store/auth-context';

function App() {
  return (
    <AuthContextProvider>
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
      <Routes />
    </AuthContextProvider>
  );
}

export default App;
