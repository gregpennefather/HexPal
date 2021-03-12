var range = 2;
var count = 0;
for (var y = -range; y <= range; y++) {
    for (var x = -range; x <= range; x++) {
        for(var z = -range; z <= range; z++) {
            if (x+y+z ===0) {
                count += 1;
            }
        }
    }
}
console.log(count);