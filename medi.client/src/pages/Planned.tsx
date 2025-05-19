
import image1 from '../assets/image1.png';
import image2 from '../assets/image2.png';

import '../styles/Planned.css';

const Przyklad: React.FC = () => (
    <>

        <div className="main-flex-container">

            <div className="left-section">

                <div className="card">
                    <div className="card-header">
                        <div className="date-time">15 sierpnia 2025, 10:00</div>
                        <div className="status-buttons">
                            <span className="status">Zaplanowana</span>
                            <button className="cancel-btn">Odwołaj</button>
                        </div>
                    </div>

                    <div className="line"></div>

                    <div className="card-row">
                        <div>
                            <div className="name">dr med. Jan Kowalski</div>
                            <div className="specialty">Kadriolog</div>
                        </div>
                        <a href="#" className="link">Specjalista</a>
                    </div>

                    <div className="line"></div>

                    <div className="card-row">
                        <div className="clinic">Przychodnia “Biegunowa”</div>
                        <a href="#" className="link">Lokalizacja</a>
                    </div>
                </div>

                <br/><br/>


                <div className="card">
                    <div className="card-header">
                        <div className="date-time">15 sierpnia 2025, 10:00</div>
                        <div className="status-buttons">
                            <span className="status">Zaplanowana</span>
                            <button className="cancel-btn">Odwołaj</button>
                        </div>
                    </div>

                    <div className="line"></div>

                    <div className="card-row">
                        <div>
                            <div className="name">dr med. Jan Kowalski</div>
                            <div className="specialty">Kadriolog</div>
                        </div>
                        <a href="#" className="link">Specjalista</a>
                    </div>

                    <div className="line"></div>

                    <div className="card-row">
                        <div className="clinic">Przychodnia “Biegunowa”</div>
                        <a href="#" className="link">Lokalizacja</a>
                    </div>
                </div>

                <br /><br />


                <div className="card">
                    <div className="card-header">
                        <div className="date-time">15 sierpnia 2025, 10:00</div>
                        <div className="status-buttons">
                            <span className="status">Zaplanowana</span>
                            <button className="cancel-btn">Odwołaj</button>
                        </div>
                    </div>

                    <div className="line"></div>

                    <div className="card-row">
                        <div>
                            <div className="name">dr med. Jan Kowalski</div>
                            <div className="specialty">Kadriolog</div>
                        </div>
                        <a href="#" className="link">Specjalista</a>
                    </div>

                    <div className="line"></div>

                    <div className="card-row">
                        <div className="clinic">Przychodnia “Biegunowa”</div>
                        <a href="#" className="link">Lokalizacja</a>
                    </div>
                </div>

                <br /><br />

            </div>

            <div className="right-section">
                <img src={image1} alt="Ilustracja 1" className="register-image"/>
                    <img src={image2} alt="Ilustracja 2" className="register-image"/>
           </div>

            </div>


            


    </>
)

export default Przyklad;
