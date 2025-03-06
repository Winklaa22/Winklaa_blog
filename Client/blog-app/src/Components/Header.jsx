import React, { useState } from 'react';
import { Link } from "react-router-dom";
import ".././Styles/Header.css";

const Header = () =>{
    return(
    <>
        <div className="header">
            <Link className="logo" to="/">Winklaa Blog</Link>
            <div className="links">
                <Link to="/register">Register</Link>
                <Link to="/login">Login</Link>
            </div>
        </div>
    </>
    );
}

export default Header