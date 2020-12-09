Windows служба FileWatcher следит за папкой Directory:
1. При добавлении файла txt формата в эту папку файл зашифровывается в UTF8 благодаря методу класса FileCrypt.cs (FileCrypt.EncryptTo).
2. Файл архивируется в папку Archived благодаря методу Archive.
3. Файл расшифруется в папку targetDirectory благодаря методу DecryptTo.
4. Файл разархивируется в папку Crypted благодаря методу UnArchive.
