import React, { useState } from 'react';
import { useLocation  ,Link,  useNavigate} from "react-router-dom";
import ".././Styles/Header.css";

const Header = () =>{
    const navigate = useNavigate();
    const token = localStorage.getItem('authToken');
    const logOut = () =>{
        localStorage.removeItem('authToken')
        navigate('/')
    }

    return(
    <>
        <div className="header">
            <Link className="logo" to="/">Winklaa Blog</Link>
            <div className="links">
                {!token ? (
                    <>
                        <Link to="/register">Register</Link>
                        <Link to="/login">Login</Link>
                    </>
                ) : (
                    <>
                        <Link to="/profile">Profile</Link>
                        <button onClick={() => logOut()}>Log Out</button>
                    </>
                )}
                
            </div>
        </div>
    </>
    );
}

export default Header