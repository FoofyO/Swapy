export interface LoginCredential {
    emailOrPhone: string,
    password: string
}

export interface UserRegistrationCredential {
    firstName: string,
    lastName: string,
    email: string,
    phoneNumber: string,
    password: string
}

export interface ShopRegistrationCredential {
    shopName: string,
    email: string,
    phoneNumber: string,
    password: string
}