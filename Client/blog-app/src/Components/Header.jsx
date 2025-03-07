import React, { useState } from 'react';
import { useLocation  ,Link } from "react-router-dom";
import ".././Styles/Header.css";

const Header = () =>{
    const location = useLocation();

    return(
    <>
        <div className="header">
            <Link className="logo" to="/">Winklaa Blog</Link>
            <div className="links">
                { location.pathname !== "/register" && (
                <Link to="/register">Register</Link>
                )}

                { location.pathname !== "/login" && (
                <Link to="/login">Login</Link>
                )}
                
            </div>
        </div>
    </>
    );
}

export default Header