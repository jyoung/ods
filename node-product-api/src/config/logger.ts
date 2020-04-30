'use strict';

import * as winston from 'winston';
import appRoot from 'app-root-path';

const options = {
    file: {
      level: 'info',
      filename: `${appRoot}/logs/app.log`,
      handleExceptions: true,
      json: true,
      maxsize: 5242880, // 5MB
      maxFiles: 5,
      colorize: false,
    },
    console: {
      level: 'debug',
      handleExceptions: true,
      json: false,
      colorize: true,
    },
  };

const logger: winston.Logger = winston.createLogger({
    format: winston.format.combine(
        winston.format.timestamp(),
        winston.format.json(),
    ),
    level: 'info',
    transports: [
        new winston.transports.File(options.file),
        new winston.transports.Console(options.console)
    ],
});

export default logger;
