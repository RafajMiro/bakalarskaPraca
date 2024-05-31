# Návrh aplikačného framework-u založeného na webovej aplikácii

Na spustenie Basyx-OTS je potrebné ako prvé otvoriť termináll v koreňovom priečinku. Potom prejdeme do priečinku Basyx-OTS zadaním príkazu cd Basyx-OTS. V termináli spustíme skript create-network.sh alebo manuálne vytvortíme sieť pomocou príkazu docker network create --driver bridge my_custom_network. Následne spustíme Docker Compose s príkazom docker-compose --build up, ktorý vytvorí a spustí kontajnery.

Na spustenie webovej aplikácie najskôr otvoríme súbor docker-compose.yml v koreňovom priečinku projektu. Nájdeme riadok s extra_hosts a upravíme hodnotu HostOfYourOPCUADevice:IPAdressOfYourOPCUADevice na konkrétnu hodnotu hostname a IP adresu nášho OPC UA zariadenia. 
Napríklad, ak je hostname zariadenia opcua-server a IP adresa 192.168.1.10, riadok by mal vyzerať takto: extra_hosts: - "opcua-server:192.168.1.10". Uložíme súbor docker-compose.yml. a vrátime do terminálu. V koreňovom priečinku projektu spustíme príkaz docker-compose --build up na vytvorenie a spustenie kontajnerov.


Webova aplikacia beží na endpointe http://localhost:4200
