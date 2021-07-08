export function SignIn() {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve({
                token: "IAShdqruhdfkjnv98252mofdsfjijdfhudshf",
                user: {
                    name: "Gustavo Ushijima",
                    email: "gustavo@ushijima.com.br",
                },
            })
        }, 2000)
    })
}
