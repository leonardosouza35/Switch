CREATE TABLE `slow_log` (
   `start_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP 
                          ON UPDATE CURRENT_TIMESTAMP,
   `user_host` mediumtext NOT NULL,
   `query_time` time NOT NULL,
   `lock_time` time NOT NULL,
   `rows_sent` int(11) NOT NULL,
   `rows_examined` int(11) NOT NULL,
   `db` varchar(512) NOT NULL,
   `last_insert_id` int(11) NOT NULL,
   `insert_id` int(11) NOT NULL,
   `server_id` int(10) unsigned NOT NULL,
   `sql_text` mediumtext NOT NULL,
   `thread_id` bigint(21) unsigned NOT NULL
  ) ENGINE=CSV DEFAULT CHARSET=utf8 COMMENT='Slow log';
  
  
  CREATE TABLE `general_log` (
   `event_time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
                          ON UPDATE CURRENT_TIMESTAMP,
   `user_host` mediumtext NOT NULL,
   `thread_id` bigint(21) unsigned NOT NULL,
   `server_id` int(10) unsigned NOT NULL,
   `command_type` varchar(64) NOT NULL,
   `argument` mediumtext NOT NULL
  ) ENGINE=CSV DEFAULT CHARSET=utf8 COMMENT='General log';
  
SET global general_log = 1;
SET global log_output = 'table';

SELECT event_time, CONVERT(argument USING utf8) FROM mysql.general_log 
where user_host='newuser1[newuser1] @ localhost [::1]'
order by (event_time) desc;

SELECT CONVERT(argument USING utf8) FROM mysql.general_log;

/*SET global general_log = 0;*/
  