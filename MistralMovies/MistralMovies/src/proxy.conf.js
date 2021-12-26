const PROXY_CONFIG = [
  {
    context: [
      "/movie"
    ],
    target: "https://localhost:7060",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
