"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.isLocalResource = void 0;
function isLocalResource(url) {
    // Ensure the URL is a string
    if (typeof url !== 'string') {
        return false;
    }
    // Trim whitespace
    url = url.trim();
    // Normalize case for consistent comparison
    const lowerUrl = url.toLowerCase();
    // Check for 'file://' protocol
    if (lowerUrl.startsWith('file://')) {
        return true;
    }
    // Check for Windows drive letter paths (e.g., 'C:/', 'D:/')
    if (/^[a-z]:[\\/]/i.test(url)) {
        return true;
    }
    // Check for UNIX-like absolute paths (e.g., '/home/user/file')
    if (lowerUrl.startsWith('/')) {
        return true;
    }
    // Check if the URL does not start with a known protocol (assumed to be relative or local)
    if (!/^(https?:|ftp:|file:|\/\/)/i.test(url)) {
        return true;
    }
    return false;
}
exports.isLocalResource = isLocalResource;
