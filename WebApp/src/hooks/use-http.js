import { useContext, useState } from 'react'
import AuthContext from '../store/auth-context';
import ENV from '../utils/env';

const useHttp = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const authCtx = useContext(AuthContext);

    const sendRequest = async (config,data, method) => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await fetch(
                `${ENV.apiURL}/${config.url}`,
                {
                    method: method ? method : "GET",
                    body: data ? JSON.stringify(data) : null,
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json',
                        'Authorization': `Bearer ${authCtx.token}`
                    }
                }
            );

            if(!response.ok) {
                throw new Error(response.json());
            }

            return response;
        } catch(error) {
            throw new Error("Invalid operation");
        }
    };

    return {
        sendRequest,
        isLoading,
        error
    }
}

export default useHttp;