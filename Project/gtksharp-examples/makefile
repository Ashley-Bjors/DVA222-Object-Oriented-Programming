dir=$$HOME/.cache/bin/gtksharp
css=$(subst .cs,,$(wildcard *.cs))

$(css): 
	@mkdir -p $(dir) && rm -f $(dir)/program.exe
	mcs *.cs -main:$@ -pkg:gtk-sharp-3.0 -out:$(dir)/program.exe
	
run:
	mono $(dir)/program.exe

clean:
	@rm -rf $(dir) *.zip

edit-all:
	code -n *.cs makefile &

edit-examples:
	code -n Example*.cs makefile &

zip: clean
	zip gtksharp-examples.zip Example*.cs *.jpg makefile
	zip gtksharp-all.zip *.cs makefile

install:
	#error if already installed 
	@dpkg -l | tr -s ' ' '\t' | cut -f2 | grep -q libgtk3.0-cil-dev && false
	#from https://www.mono-project.com/download/stable/ for Ubuntu users
	sudo apt install ca-certificates gnupg
	sudo gpg --homedir /tmp --no-default-keyring --keyring /usr/share/keyrings/mono-official-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
	echo "deb [signed-by=/usr/share/keyrings/mono-official-archive-keyring.gpg] https://download.mono-project.com/repo/ubuntu stable-focal main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list
	sudo apt update && sudo apt-get install mono-devel libgtk3.0-cil-dev
