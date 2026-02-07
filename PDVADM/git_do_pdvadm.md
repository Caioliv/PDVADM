# 🧾 Git do PDVADM

Checklist oficial para versionamento do projeto **PDVADM**.
Use sempre este roteiro para evitar problemas com Git, Visual Studio e arquivos indevidos no repositório.

---

## 📍 1️⃣ Sempre comece no lugar certo

```bat
cd C:\Projetos\PDVADM
```

> ❗ Nunca execute comandos Git fora desta pasta.

---

## 👀 2️⃣ Ver o que mudou

```bat
git status
```

Nunca pule este passo. Ele mostra:
- arquivos modificados
- arquivos novos
- arquivos que **não deveriam estar no Git**

---

## ➕ 3️⃣ Adicionar arquivos

### Tudo de uma vez (padrão)
```bat
git add .
```

### Apenas um módulo específico
```bat
git add PDVADM.Application
```

---

## 🧪 4️⃣ Conferir antes de commitar

```bat
git status
```

O esperado é algo como:
```
Changes to be committed
```

---

## 📝 5️⃣ Commit (mensagem clara)

```bat
git commit -m "FastSale: descrição objetiva do que foi feito"
```

### Padrões de mensagem recomendados
- **feat:** nova funcionalidade
- **fix:** correção de bug
- **chore:** limpeza, git, csproj, infra

Exemplos:
- `feat: fluxo completo de FastSale`
- `fix: correção de DI no Application`
- `chore: cleanup gitignore e arquivos temporários`

---

## 🚀 6️⃣ Push para o GitHub

```bat
git push origin main
```

---

## 🚫 Regras de ouro do PDVADM

❌ Nunca versionar:
- `.vs/`
- `Backup/`
- `UpgradeLog.htm`
- `bin/`
- `obj/`

❌ Nunca rodar `git add .` fora da pasta do repositório

✅ Sempre confiar no comando:
```bat
git status
```

Se ele retornar:
```
nothing to commit, working tree clean
```
👉 Repositório saudável ✅

---

## 🧠 Observação final

Se aparecer algo com `../` no `git status`, **pare** e revise o diretório atual.
Isso indica que arquivos fora do repositório estão sendo rastreados por engano.

Este checklist é o ritual oficial do projeto **PDVADM** 🚀

