import axios from "axios"
const https = require("https")

const httpClient = axios.create({
    baseUrl: "http://localhost:3333",
    httpsAgent: new https.Agent({
        rejectUnauthorized: false,
    }),
})

//httpClient.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';
httpClient.defaults.headers.get["Accepts"] = "application/json"

const mainInterceptor = httpClient.interceptors.request.use(
    (config) => {
        //config.headers['apiKey'] = '';
        return config
    },
    (error) => {
        // handle the error
        return Promise.reject(error)
    }
)

export default httpClient
