import React, { useEffect } from 'react';

interface AlertMessageProps {
    message: { type: 'success' | 'error'; text: string } | null;
    setMessage: React.Dispatch<React.SetStateAction<{ type: 'success' | 'error'; text: string } | null>>;
}

const AlertMessage: React.FC<AlertMessageProps> = ({ message, setMessage }) => {
    useEffect(() => {
        if (message) {
            const timer = setTimeout(() => {
                setMessage(null);
            }, 5000); 
            return () => clearTimeout(timer);
        }
    }, [message, setMessage]);

    if (!message) return null;

    return (
        <div
            className={`
                alert alert-${message.type === 'success' ? 'success' : 'danger'}
                alert-dismissible fade show
                position-absolute top-0 start-50 translate-middle-x mt-3
                w-100 w-sm-75 w-md-50 w-lg-25
                px-3
                shadow
                z-3
              `}
            style={{ maxWidth: '500px' }}
            role="alert"
        >
            {message.text}
            <button
                type="button"
                className="btn-close"
                onClick={() => setMessage(null)}
                aria-label="Close"
            ></button>
        </div>
    );
};

export default AlertMessage;
