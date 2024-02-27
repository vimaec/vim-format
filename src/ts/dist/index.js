"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __exportStar = (this && this.__exportStar) || function(m, exports) {
    for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(exports, p)) __createBinding(exports, m, p);
};
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.VimHelpers = void 0;
// Links files to generate package type exports
__exportStar(require("./bfast"), exports);
__exportStar(require("./g3d/g3d"), exports);
__exportStar(require("./remoteVimx"), exports);
__exportStar(require("./g3d/g3dMaterials"), exports);
__exportStar(require("./g3d/g3dMesh"), exports);
__exportStar(require("./g3d/g3dChunk"), exports);
__exportStar(require("./g3d/g3dScene"), exports);
__exportStar(require("./http/remoteBuffer"), exports);
__exportStar(require("./http/requestTracker"), exports);
__exportStar(require("./http/requester"), exports);
__exportStar(require("./http/remoteValue"), exports);
__exportStar(require("./vimHeader"), exports);
__exportStar(require("./objectModel"), exports);
__exportStar(require("./structures"), exports);
exports.VimHelpers = __importStar(require("./vimHelpers"));
