import React from 'react';

type ModalProps = {
    onClose: () => void;
    children: React.ReactNode; 
    modalTitle: string;
};

const Modal: React.FC<ModalProps> = ({ modalTitle ,onClose, children }) => {
    return (
        <div
            className="modal fade show d-block"
            tabIndex={-1}
            role="dialog"
            style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}
            onClick={e => {
                if (e.target === e.currentTarget) onClose();
            }}
        >
            <div
                className="modal-dialog modal-dialog-centered"
                id="custom-modal-dialog"
                role="document"
            >
                <div className="modal-content d-flex flex-column" style={{ height: '100%' }}>
                    <div className="modal-header">
                        <h5 className="modal-title">{modalTitle}</h5>
                        <button
                            type="button"
                            className="btn-close"
                            aria-label="Close"
                            onClick={onClose}
                        />
                    </div>
                    <div className="modal-body flex-grow-1 pb-5">
                        {children}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Modal;
