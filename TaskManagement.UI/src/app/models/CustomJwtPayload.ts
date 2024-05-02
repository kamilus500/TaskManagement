import { JwtPayload } from "jwt-decode";

export interface CustomJwtPayload extends JwtPayload {
    Login: string;
    UserId: string;
}