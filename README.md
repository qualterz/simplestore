# SimpleStore

## Docker

```bash
# Build image
docker build --tag simplestore --file Dockerfile .
# Run image
docker run --network=host simplestore
```

Once started, the website available at http://localhost

Administration panel available at http://localhost/Administration

To login input username `admin` and password `admin`

<details>
  <summary>How it looks like</summary>

  ![Admin Panel](https://user-images.githubusercontent.com/38355785/177038149-b0fef10a-6d25-4698-8791-0413c8f9ab08.png)
  ![Home](https://user-images.githubusercontent.com/38355785/177038145-0649766c-066d-493e-abcc-7d1e0fbf625c.png)
  ![Item Overview](https://user-images.githubusercontent.com/38355785/177038147-40029708-e0cf-4c70-bab6-52d79ce0eb44.png)
</details>