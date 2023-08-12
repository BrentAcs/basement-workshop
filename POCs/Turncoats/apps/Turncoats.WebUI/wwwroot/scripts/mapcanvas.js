function getDivCanvasClientInfo(el) {
    var obj = {};
    obj.clientWidth = el.clientWidth;
    obj.clientHeight = el.clientHeight;
    obj.offsetLeft = el.offsetLeft;
    obj.offsetTop = el.offsetTop;
    return JSON.stringify(obj);
}
