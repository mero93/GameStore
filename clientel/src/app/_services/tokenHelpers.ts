import jwt_decode from 'jwt-decode';

export function decodeUserId() {
  let token = JSON.parse(localStorage.getItem('user')).token;
  let userId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
  return jwt_decode(token)[userId];
}