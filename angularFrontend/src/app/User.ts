export interface UserDetails {
    name: string,
    email: string,
    sessionToken: string,
    role: boolean
}

export interface SuccessfulUserSignupData {
    id: string,
    name: string,
    email: string,
    password: string
}