import { useState } from "react";
import { JwtResponse } from "../models/auth/jwt.respone";

export default function useToken() {
  
  function saveToken(token: JwtResponse) {
    localStorage.setItem('token', JSON.stringify(token));
    setToken(token);
  }
  
  function getToken() : JwtResponse | null {
    const tokenString = localStorage.getItem('token');
    if(tokenString){
      return JSON.parse(tokenString);
    }
    return null;
  }

  const [token,setToken] = useState<JwtResponse | null>(getToken());
  
  return{
    setToken: saveToken,
    token
  }
}