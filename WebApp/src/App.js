import Routes from './components/core/Routes/Routes';
import { AuthContextProvider } from './store/auth-context';

function App() {
  return (
    <AuthContextProvider>
      <Routes />
    </AuthContextProvider>
  );
}

export default App;
