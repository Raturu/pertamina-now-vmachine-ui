import requests

url = "http://159.65.139.83/pertamina-now/api/Collection/listBBM/"
headers = {"x-api-key": "CODEX@123"}
payload = {"id_spbu": 1}

req = requests.post(url, data=payload, headers=headers)

file = open("gasoline.json","w")
file.write(req.text)