ObjectPoolingManager.Instance.CreatePool (m_bullet, 50, 50);
ObjectPoolingManager.Instance.CreatePool (m_grunt, 50, 50);
ObjectPoolingManager.Instance.CreatePool (m_grunt_explosion, 50, 50);
ObjectPoolingManager.Instance.CreatePool (m_hulk, 50, 50);

// instantiate
GameObject go = ObjectPoolingManager.Instance.GetObject (m_grunt.name);

// destroy
go.SetActive(false)
