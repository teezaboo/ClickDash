const { message, result, createDataItemSigner } = require('@permaweb/aoconnect');
const fs = require('fs');

// โหลดไฟล์ wallet.json
function loadWallet(path) {
    const walletJson = fs.readFileSync(path);
    return JSON.parse(walletJson); // แปลง JSON เป็น Object
}

// ฟังก์ชัน submitScore สำหรับส่งคะแนน
async function submitScore(username, score, walletPath) {
    const wallet = loadWallet(walletPath); // โหลดไฟล์ wallet.json
    await message({
        process: 'process-id', // ใส่ process ID ที่เกี่ยวข้อง
        tags: [
            { name: 'Username', value: username },
            { name: 'Score', value: score.toString() }
        ],
        signer: createDataItemSigner(wallet) // ใช้ wallet ที่โหลดมาในการเซ็นชื่อ
    });
}

// ฟังก์ชัน getLeaderboard สำหรับดึงข้อมูลลีดเดอร์บอร์ด
async function getLeaderboard() {
    const resultsOut = await results({
        process: 'process-id',
        sort: 'DESC', // เรียงลำดับจากมากไปน้อย
        limit: 10 // จำกัดผลลัพธ์ที่ 10 อันดับ
    });
    return resultsOut;
}

// ตัวอย่างการเรียกใช้ submitScore
const walletPath = 'wallet.json'; // ใช้ / แทน
submitScore('Player1', 1000, walletPath);
